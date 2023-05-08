import {Component, Input, OnInit} from '@angular/core';
import {Genre} from "../../models/Genre";
import {PlatformType} from "../../models/PlatformType";
import {GenreService} from "../../services/genre.service";
import {PlatformTypeService} from "../../services/platform-type.service";
import {GameService} from "../../../core/services/game.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {GetPublisherBriefResponse} from "../../../publishers/models/GetPublisherBriefResponse";
import {PublisherService} from "../../../core/services/publisher.service";
import {HierarchicalDataService} from "../../../core/services/hierarchical-data.service";
import {CheckboxListItem} from "../../../shared/models/CheckBoxListItem";
import {DropDownItem} from "../../../shared/models/DropDownItem";
import {forkJoin} from "rxjs";

@Component({
    selector: 'app-create-game-page',
    templateUrl: './create-game-page.component.html',
    styleUrls: ['./create-game-page.component.scss']
})
export class CreateGamePageComponent implements OnInit {
    genres!: CheckboxListItem[];
    platformTypes!: CheckboxListItem[];
    publishers!: DropDownItem[];

    constructor(
        private genreService: GenreService,
        private platformTypeService: PlatformTypeService,
        private publisherService: PublisherService,
        private hierarchicalDataService: HierarchicalDataService
    ) {
    }

    ngOnInit(): void {
        this.getData();
    }

    private getData(): void {
        forkJoin([
            this.genreService.getAllGenres(),
            this.platformTypeService.getAllPlatformTypes(),
            this.publisherService.getAllPublishersBrief()
        ]).subscribe(([genres, platformTypes, publishers]: [Genre[], PlatformType[], GetPublisherBriefResponse[]]) => {
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
                value: publisher.CompanyName
            }));
        });
    }
}
