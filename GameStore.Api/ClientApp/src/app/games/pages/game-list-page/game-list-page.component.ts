import {Component, Input} from '@angular/core';
import {GetGameResponse} from "../../models/GetGameResponse";
import {FilterGameRequest} from "../../models/FilterGameRequest";
import {forkJoin, switchMap, tap} from "rxjs";
import {Genre} from "../../models/Genre";
import {PlatformType} from "../../models/PlatformType";
import {GetPublisherBriefResponse} from "../../../publishers/models/GetPublisherBriefResponse";
import {CheckboxListItem} from "../../../shared/models/CheckBoxListItem";
import {GenreService} from "../../services/genre.service";
import {PlatformTypeService} from "../../services/platform-type.service";
import {PublisherService} from "../../../publishers/services/publisher.service";
import {HierarchicalDataService} from "../../../core/services/hierarchical-data.service";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {ActivatedRoute, Router} from "@angular/router";
import {SortOption} from "../../models/SortOption";
import {DateFilterOption} from "../../models/DateFilterOption";
import {GameService} from "../../services/game.service";

@Component({
    selector: 'app-game-list-page',
    templateUrl: './game-list-page.component.html',
    styleUrls: ['./game-list-page.component.scss']
})
export class GameListPageComponent {
    paginatedGames!: PaginationResult<GetGameResponse>;
    genres!: CheckboxListItem[];
    platformTypes!: CheckboxListItem[];
    publishers!: CheckboxListItem[];
    sortOptions!: DropDownItem[];
    dateFilterOption!: DropDownItem[];
    pageSizes!: DropDownItem[];
    filterGameRequest!: FilterGameRequest;

    constructor(
        private gameService: GameService,
        private genreService: GenreService,
        private platformTypeService: PlatformTypeService,
        private publisherService: PublisherService,
        private hierarchicalDataService: HierarchicalDataService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.filterGameRequest = new FilterGameRequest()
        this.getData();
    }

    private getData(): void {
        forkJoin([
            this.genreService.getAllGenres(),
            this.platformTypeService.getAllPlatformTypes(),
            this.publisherService.getAllPublishersBrief()
        ]).subscribe(([genres, platformTypes, publishers]: [Genre[], PlatformType[], GetPublisherBriefResponse[]]) => {
            this.getGamesOfCurrentPage();

            this.genres = this.hierarchicalDataService.convertToTreeStructure<Genre, CheckboxListItem>(
                genres,
                'Id',
                'ParentGenreId',
                (genre) => ({
                    id: genre.Id,
                    label: genre.Name,
                    checked: false,
                    parentId: genre.ParentGenreId
                })
            );

            this.platformTypes = platformTypes.map((type: PlatformType) => ({
                id: type.Id,
                label: type.Type,
                checked: false,
            }));

            this.publishers = publishers.map((publisher: GetPublisherBriefResponse) => ({
                id: publisher.Id,
                label: publisher.CompanyName,
                checked: false,
            }));
        });

        this.sortOptions = [
            {id: SortOption.MostViewed, value: 'Most viewed'},
            {id: SortOption.MostCommented, value: 'Most commented'},
            {id: SortOption.PriceDescending, value: 'Price descending'},
            {id: SortOption.PriceAscending, value: 'Price ascending'},
            {id: SortOption.New, value: 'New'},
        ]

        this.dateFilterOption = [
            {id: DateFilterOption.None, value: 'All'},
            {id: DateFilterOption.LastWeek, value: 'Last week'},
            {id: DateFilterOption.LastMonth, value: 'Last month'},
            {id: DateFilterOption.LastYear, value: 'Last year'},
            {id: DateFilterOption.TwoYears, value: 'Two years ago'},
            {id: DateFilterOption.ThreeYears, value: 'Three years ago'},
        ]

        this.pageSizes = [
            {id: 10, value: '10'},
            {id: 20, value: '20'},
            {id: 50, value: '50'},
            {id: 100, value: '100'},
            {id: -1, value: 'All'},
        ]

        this.onPageChanged();
    }

    onFilter(filter: FilterGameRequest): void {
        const mergedFilters = {...this.filterGameRequest, ...filter};
        if(JSON.stringify(this.filterGameRequest) === JSON.stringify(mergedFilters)){
            this.navigateToFirstPage();
            return;
        }
        Object.assign(this.filterGameRequest, mergedFilters);
        this.navigateToFirstPage();
        this.getGames()
    }

    onPageChanged(): void {
        this.getGamesOfCurrentPage();
    }

    private getGames(): void{
        this.gameService.getGames(this.filterGameRequest).subscribe((paginatedGames: PaginationResult<GetGameResponse>) =>{
            this.paginatedGames = paginatedGames;
        })
    }

    private getGamesOfCurrentPage(): void {
        this.route.paramMap.pipe(
            tap(params => {
                const pageNumber = +params.get('page')!;
                if (isNaN(pageNumber)) {
                    this.navigateToFirstPage()
                } else {
                    this.filterGameRequest.PageNumber = pageNumber;
                }
            }),
            switchMap(() => this.gameService.getGames(this.filterGameRequest))
        ).subscribe((paginatedGames: PaginationResult<GetGameResponse>) => {
            this.paginatedGames = paginatedGames;
        });
    }

    private navigateToFirstPage(): void{
        this.router.navigate(['/game/list/1']);
    }
}
