import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BsModalRef} from "ngx-bootstrap/modal";

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.scss']
})
export class ConfirmationModalComponent {
    @Input() title!: string;
    @Input() message!: string;
    @Input() btnOkText!: string;
    @Input() btnCancelText!: string;

    @Output() confirm = new EventEmitter();

    constructor(public modalRef: BsModalRef) { }

    confirmAndClose() {
        this.confirm.emit();
        this.modalRef.hide();
    }
}
