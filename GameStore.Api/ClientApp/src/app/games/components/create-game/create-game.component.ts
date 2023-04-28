import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {CreateGameRequest} from "../../../core/models/CreateGameRequest";
import {CheckboxControlValueAccessor, FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Genre} from "../../../core/models/Genre";
import {PlatformType} from "../../../core/models/PlatformType";
import {GetPublisherBriefResponse} from "../../../core/models/GetPublisherBriefResponse";

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit{
  @ViewChild("parentGenreRef") parentGenreRef!:  ElementRef<HTMLInputElement>;
  @Input() genres!: Genre[];
  @Input() platformTypes!: PlatformType[];
  @Input() publishers!: GetPublisherBriefResponse[];
  @Output() gameCreated = new EventEmitter<CreateGameRequest>();
  createGameForm!: FormGroup

  constructor(private fb: FormBuilder) {}
  ngOnInit(): void {
    this.createGameForm = this.fb.group({
      Name: ['', Validators.required],
      Description: ['', [Validators.required, Validators.minLength(50)]],
      GenreIds: [[], Validators.required],
      PlatformTypeIds: [[], Validators.required],
      PublisherId:['',Validators.required],
      Price: ['', [Validators.required, Validators.min(0.01)]],
      UnitsInStock: ['', [Validators.required, Validators.min(1)]]
    });
  }

  onSubmit() {
    this.gameCreated.emit(this.createGameForm.value);
  }

  onCheckBoxChange(event: any, id: number, formControlName: string){
    const entityIds = this.createGameForm.get(formControlName) as FormControl;
    let updatedGenreIds = [];
    if(event.target.checked){
      updatedGenreIds = [...entityIds.value, id];
    }else {
      updatedGenreIds = entityIds.value.filter((x: number) => x !== id);
    }
    entityIds.setValue(updatedGenreIds);
    entityIds.markAsTouched();
  }
}
