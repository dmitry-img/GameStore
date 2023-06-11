import {Component, OnInit} from '@angular/core';
import {CheckboxListItem} from "../../../shared/models/CheckBoxListItem";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {GenreService} from "../../../genres/services/genre.service";
import {PlatformTypeService} from "../../services/platform-type.service";
import {PublisherService} from "../../../publishers/services/publisher.service";
import {HierarchicalDataService} from "../../../core/services/hierarchical-data.service";
import {forkJoin, switchMap} from "rxjs";
import {GetGenreResponse} from "../../../genres/models/GetGenreResponse";
import {PlatformType} from "../../models/PlatformType";
import {GetPublisherBriefResponse} from "../../../publishers/models/GetPublisherBriefResponse";
import {ActivatedRoute} from "@angular/router";
import {GameService} from "../../services/game.service";
import {GetGameBriefResponse} from "../../models/GetGameBriefResponse";
import {GetGameResponse} from "../../models/GetGameResponse";
import {AuthService} from "../../../auth/services/auth.service";

@Component({
  selector: 'app-update-game-page',
  templateUrl: './update-game-page.component.html',
  styleUrls: ['./update-game-page.component.scss']
})
export class UpdateGamePageComponent implements OnInit{
    genres!: CheckboxListItem[];
    platformTypes!: CheckboxListItem[];
    publishers!: DropDownItem[];
    discontinuedOptions!: DropDownItem[];
    game!: GetGameResponse;
    userRole!: string | null;

    constructor(
        private genreService: GenreService,
        private platformTypeService: PlatformTypeService,
        private publisherService: PublisherService,
        private gameService: GameService,
        private authService: AuthService,
        private route: ActivatedRoute,
        private hierarchicalDataService: HierarchicalDataService
    ) { }

    ngOnInit(): void {
        this.getData();
    }

    private getData(): void {
        this.route.params.pipe(
            switchMap((params) =>
                forkJoin([
                    this.gameService.getGameByKey(params['key']),
                    this.genreService.getAllGenres(),
                    this.platformTypeService.getAllPlatformTypes(),
                    this.publisherService.getAllPublishersBrief()
                ])
            )
        ).subscribe(([game, genres, platformTypes, publishers]) => {
            this.game = game;

            this.genres = this.hierarchicalDataService.convertToTreeStructure<GetGenreResponse, CheckboxListItem>(
                genres,
                'Id',
                'ParentGenreId',
                (genre) => ({
                    id: genre.Id,
                    label: genre.Name,
                    checked: game.Genres.some(g => g.Id === genre.Id || g.ParentGenreId === genre.Id),
                    parentId: genre.ParentGenreId
                })
            ).filter((item: CheckboxListItem) => item.label !== 'Other');

            this.platformTypes = platformTypes.map((type: PlatformType) => ({
                id: type.Id,
                label: type.Type,
                checked: game.PlatformTypes.some(pt => pt.Id === type.Id),
            }));

            this.publishers = publishers.map((publisher: GetPublisherBriefResponse) => ({
                Id: publisher.Id,
                Value: publisher.CompanyName
            }));

            this.publishers.unshift({Id: null, Value: 'Unknown'})


        });

        this.userRole = this.authService.decodeAccessToken()!.Role;

        this.discontinuedOptions = [
            { Id: false, Value: 'No' },
            { Id: true, Value: 'Yes' }
        ]
    }
}
