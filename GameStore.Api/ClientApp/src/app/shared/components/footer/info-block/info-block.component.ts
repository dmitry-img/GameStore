import {Component, EventEmitter, Input, Output} from '@angular/core';
import { faEnvelope } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-info-block',
  templateUrl: './info-block.component.html',
  styleUrls: ['./info-block.component.scss']
})
export class InfoBlockComponent {
    @Input() imageSrc!: string;
    @Input() imageAlt!: string;
    @Input() title!: string;
    @Input() buttonText!: string;
    @Input() contentBackgroundColor!: string;
    @Output() submit: EventEmitter<void> = new EventEmitter<void>();

    onSubmit(): void {
        this.submit.emit();
    }
}
