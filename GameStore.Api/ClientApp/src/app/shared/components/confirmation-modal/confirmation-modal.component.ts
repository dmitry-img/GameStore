import {Component, EventEmitter, Input, Output, TemplateRef} from '@angular/core';
import {BsModalRef} from "ngx-bootstrap/modal";

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.scss']
})
export class ConfirmationModalComponent {
    @Input() title!: string;
    @Input() btnOkText!: string;
    @Input() btnCancelText!: string;
    @Input() content!: TemplateRef<any>;

    @Output() confirm = new EventEmitter();

    constructor(public modalRef: BsModalRef) { }

    onConfirm(): void {
        this.confirm.emit();
    }
}
