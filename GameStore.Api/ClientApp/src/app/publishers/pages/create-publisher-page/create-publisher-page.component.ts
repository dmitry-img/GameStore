import {Component, EventEmitter, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {CreatePublisherRequest} from "../../models/CreatePublisherRequest";
import {CreateGameRequest} from "../../../games/models/CreateGameRequest";
import {PublisherService} from "../../../core/services/publisher.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-publisher-page',
  templateUrl: './create-publisher-page.component.html',
  styleUrls: ['./create-publisher-page.component.scss']
})
export class CreatePublisherPageComponent {
}
