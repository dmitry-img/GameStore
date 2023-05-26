import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-pagination-nav',
  templateUrl: './pagination-nav.component.html',
  styleUrls: ['./pagination-nav.component.scss']
})
export class PaginationNavComponent {
    @Input() baseUrl!: string;
    @Input() totalItems!: number;
    @Input() itemsPerPage!: number;
    @Input() currentPage!: number;

    get totalPageCount(): number {
        return Math.ceil(this.totalItems / this.itemsPerPage);
    }

    get pageNumbers(): number[] {
        return Array(this.totalPageCount).fill(0).map((x,i)=>i+1);
    }
}
