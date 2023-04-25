import {Component, Input, OnInit} from '@angular/core';
import {Genre} from "../../../core/models/Genre";
import {PlatformType} from "../../../core/models/PlatformType";
import {GenreService} from "../../services/genre.service";
import {PlatformTypeService} from "../../services/platform-type.service";
import {GameService} from "../../../core/service/game.service";
import {CreateGameRequest} from "../../../core/models/CreateGameRequest";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {GetPublisherBriefResponse} from "../../../core/models/GetPublisherBriefResponse";
import {PublisherService} from "../../../core/service/publisher.service";

@Component({
  selector: 'app-create-game-page',
  templateUrl: './create-game-page.component.html',
  styleUrls: ['./create-game-page.component.scss']
})
export class CreateGamePageComponent implements OnInit{
  genres!: Genre[];
  platformTypes!: PlatformType[];
  publishers!: GetPublisherBriefResponse[];
  constructor(
    private gameService: GameService,
    private genreService: GenreService,
    private platformTypeService: PlatformTypeService,
    private publisherService: PublisherService,
    private router: Router,
    private toaster: ToastrService) { }

  ngOnInit(): void {
    this.genreService.getAllGenres().subscribe((genres: Genre[]) =>{
      this.genres = genres;
    });

    this.platformTypeService.getAllPlatformTypes().subscribe((platformTypes: PlatformType[]) =>{
      this.platformTypes = platformTypes;
    });

    this.publisherService.getAllPublishersBrief().subscribe((publishers:GetPublisherBriefResponse[]) =>{
      this.publishers = publishers;
    })
  }

  onGameCreated(newGame: CreateGameRequest) {
    this.gameService.createGame(newGame).subscribe({
      next: () =>{
        this.toaster.success("The game has been created successfully!")
        this.router.navigate(['/'])
      },
      error: (error)=>{
        const errorArray = error.error.Message.split(',');
        errorArray.forEach((message: string) => {
          this.toaster.error(message);
        })
      }
    });
  }
}
