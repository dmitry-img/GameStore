import {Component, Input} from '@angular/core';
import {GetGameResponse} from "../../models/GetGameResponse";
import {FilterGameRequest} from "../../models/FilterGameRequest";
import {forkJoin, switchMap, tap} from "rxjs";
import {GetGenreResponse} from "../../../genres/models/GetGenreResponse";
import {PlatformType} from "../../models/PlatformType";
import {GetPublisherBriefResponse} from "../../../publishers/models/GetPublisherBriefResponse";
import {CheckboxListItem} from "../../../shared/models/CheckBoxListItem";
import {PlatformTypeService} from "../../services/platform-type.service";
import {PublisherService} from "../../../publishers/services/publisher.service";
import {HierarchicalDataService} from "../../../core/services/hierarchical-data.service";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {PaginationResult} from "../../../shared/models/PaginationResult";
import {ActivatedRoute, Router} from "@angular/router";
import {SortOption} from "../../models/SortOption";
import {DateFilterOption} from "../../models/DateFilterOption";
import {GameService} from "../../services/game.service";
import {GenreService} from "../../../genres/services/genre.service";

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
        ]).subscribe(([genres, platformTypes, publishers]: [GetGenreResponse[], PlatformType[], GetPublisherBriefResponse[]]) => {
            this.getGamesOfCurrentPage();

            this.genres = this.hierarchicalDataService.convertToTreeStructure<GetGenreResponse, CheckboxListItem>(
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
            {Id: SortOption.MostViewed, Value: 'Most viewed'},
            {Id: SortOption.MostCommented, Value: 'Most commented'},
            {Id: SortOption.PriceDescending, Value: 'Price descending'},
            {Id: SortOption.PriceAscending, Value: 'Price ascending'},
            {Id: SortOption.New, Value: 'New'},
        ]

        this.dateFilterOption = [
            {Id: DateFilterOption.None, Value: 'All'},
            {Id: DateFilterOption.LastWeek, Value: 'Last week'},
            {Id: DateFilterOption.LastMonth, Value: 'Last month'},
            {Id: DateFilterOption.LastYear, Value: 'Last year'},
            {Id: DateFilterOption.TwoYears, Value: 'Two years ago'},
            {Id: DateFilterOption.ThreeYears, Value: 'Three years ago'},
        ]

        this.pageSizes = [
            {Id: 10, Value: '10'},
            {Id: 20, Value: '20'},
            {Id: 50, Value: '50'},
            {Id: 100, Value: '100'},
            {Id: -1, Value: 'All'},
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
                    this.filterGameRequest.Pagination.PageNumber = pageNumber;
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
