import { Component } from '@angular/core';
import {IconDefinition} from "@fortawesome/free-solid-svg-icons";
import {faCopyright} from "@fortawesome/free-regular-svg-icons";

@Component({
  selector: 'app-main-footer',
  templateUrl: './main-footer.component.html',
  styleUrls: ['./main-footer.component.scss']
})
export class MainFooterComponent {
    copyrightIcon: IconDefinition = faCopyright
}
