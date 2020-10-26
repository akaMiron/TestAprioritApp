import { Component, EventEmitter, Inject, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PositionService } from '../services/position.service';
import { Position } from '../models/position';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-position-add-edit',
  templateUrl: './position-add-edit.component.html',
  styleUrls: ['./position-add-edit.component.scss']
})
export class PositionAddEditComponent implements OnInit {

  form: FormGroup;
  actionType: string;
  formTitle: string;
  positionId: number;
  errorMessage: any;
  existingPosition: Position;

  result: Position;

  @ViewChild('closePositionModal') closeModal;

  @Output() emitSave = new EventEmitter<boolean>();
  close(): void {
      this.emitSave.emit();
  }

  constructor(
    private positionService: PositionService,
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router,
    public dialogRef: MatDialogRef<PositionAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Position) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formTitle = 'title';
    this.result = new Position();
    if (this.avRoute.snapshot.params[idParam]) {
      this.positionId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        positionId: 0,
        title: ['', [Validators.required]],
      }
    );
  }

  ngOnInit(): void {

    if (this.positionId > 0) {
      this.actionType = 'Edit';
      this.positionService.getPosition(this.positionId)
        .subscribe(data => (
          this.existingPosition = data,
          this.form.controls[this.formTitle].setValue(data.title)
        ));
    }
  }


  onNoClick(): void {
    this.dialogRef.close();
  }

  save(): void {
    if (!this.form.valid) {
      return;
    } else {
      const position: Position = {
          title: this.form.get(this.formTitle).value
      };
      this.dialogRef.close(position);
    }
  }

  get title(): any { return this.form.get(this.formTitle); }

}
