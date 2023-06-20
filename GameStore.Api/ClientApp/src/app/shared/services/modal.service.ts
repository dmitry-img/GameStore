import { Injectable } from '@angular/core';
import {Router} from "@angular/router";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {InfoModalComponent} from "../components/info-modal/info-modal.component";
import {ConfirmationModalComponent} from "../components/confirmation-modal/confirmation-modal.component";
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class ModalService {
    private modalRef!: BsModalRef;

    constructor(
        private modalService: BsModalService,
        private router: Router,
        private toaster: ToastrService) {}

    openInfoModalWithRedirection(title: string, content: string, redirectionPath: string = '/'): void {
        const initialState = {
            title,
            content
        };

        this.modalRef = this.modalService.show(InfoModalComponent, { initialState });
        this.modalRef.content.hideEvent.subscribe(() => {
            this.modalRef.hide();
            this.router.navigate([redirectionPath]);
        });
    }
}
