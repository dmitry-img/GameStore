import {Component, EventEmitter, Input, OnDestroy, Output} from '@angular/core';
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";

@Component({
  selector: 'app-info-modal',
  templateUrl: './info-modal.component.html',
  styleUrls: ['./info-modal.component.scss']
})
export class InfoModalComponent implements OnDestroy{
    @Input() title!: string;
    @Input() content!: string;
    hideEvent: EventEmitter<void> = new EventEmitter();

    constructor(public modalRef: BsModalRef) { }

    onHide(): void {
        this.hideEvent.next();
    }

    ngOnDestroy(): void{
        this.hideEvent.next();
    }
}
