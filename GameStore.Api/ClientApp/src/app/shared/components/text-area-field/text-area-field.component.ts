import {AfterViewInit, Component, ElementRef, Input, OnChanges, OnInit, SimpleChanges, ViewChild} from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
    selector: 'app-text-area-field',
    templateUrl: './text-area-field.component.html',
    styleUrls: ['./text-area-field.component.scss']
})
export class TextAreaFieldComponent implements OnInit, AfterViewInit, OnChanges{
    @Input() control!: FormControl;
    @Input() isRequired!: boolean;
    @Input() labelText!: string
    @Input() immutableText!: string;
    @ViewChild('inputTextArea') inputTextArea!: ElementRef;

    private immutableTextInQuotes: string = '';
    private immutableTextLength: number = 0;

    ngOnInit(): void {
        this.updateImmutableText();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['immutableText']) {
            this.updateImmutableText();
            if (this.inputTextArea) {
                this.inputTextArea.nativeElement.value = this.immutableTextInQuotes;
            }
        }
    }

    private updateImmutableText(): void {
        this.immutableTextInQuotes = this.immutableText && this.immutableText.trim() !== '' ? `"${this.immutableText}" ` : '';
        this.immutableTextLength = this.immutableTextInQuotes.length;
    }

    ngAfterViewInit(): void {
        this.inputTextArea.nativeElement.value = this.immutableTextInQuotes;
    }

    onKeydown(event: any): void {
        const cursorPosition = event.target.selectionStart;
        if ((event.key === "Backspace" || event.key === "Delete") && cursorPosition <= this.immutableTextLength) {
            event.preventDefault();
        }
    }

    onInput(event: any): void {
        const value = event.target.value;
        if (!value.startsWith(this.immutableTextInQuotes)) {
            event.target.value = this.immutableTextInQuotes + value.slice(this.immutableTextLength);
        }
        this.control.setValue(value.slice(this.immutableTextLength));
    }
}
