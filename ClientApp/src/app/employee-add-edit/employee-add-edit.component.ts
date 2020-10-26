import { Component, EventEmitter, Inject, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { PositionService } from '../services/position.service';
import { Position } from '../models/position';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeePositionViewModel } from '../models/EmployeePositionViewModel';
import { EmployeePosition } from '../models/employeePosition';

@Component({
  selector: 'app-employee-add-edit',
  templateUrl: './employee-add-edit.component.html',
  styleUrls: ['./employee-add-edit.component.scss']
})
export class EmployeeAddEditComponent implements OnInit {

  form: FormGroup;

  formFirstName: string;
  formLastName: string;
  formPosition: string;
  formSalary: string;
  formHired: string;
  formFired: string;

  selectedPosition: number;

  employeeId: number;
  errorMessage: any;
  // existingEmployee: Employee;
  positions: Observable<Position[]>;

  result: EmployeePositionViewModel;

  constructor(
    private employeeService: EmployeeService,
    private positionService: PositionService,
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router,
    public dialogRef: MatDialogRef<EmployeeAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Employee) {
    const idParam = 'id';
    this.formFirstName = 'firstName';
    this.formLastName = 'lastName';
    this.formPosition = 'position';
    this.formSalary = 'salary';
    this.formFired = 'fired';
    this.formHired = 'hired';
    this.result = new EmployeePositionViewModel();
    if (this.avRoute.snapshot.params[idParam]) {
      this.employeeId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        employeeId: 0,
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        salary: ['', [Validators.required]],
        hired: ['', [Validators.required]],
        position: ['', [Validators.required]],
        fired: []
      }
    );
  }

  ngOnInit(): void {
    this.positions = this.positionService.getPositions();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  save(): void {
    if (!this.form.valid) {
      return;
    } else {
      const employee: EmployeePositionViewModel = {
          employee: {
            firstName: this.form.get(this.formFirstName).value,
            lastName: this.form.get(this.formLastName).value,
          },
          employeePosition: {
            employeePositionId: 0,
            employeeId: 0,
            salary: this.form.get(this.formSalary).value,
            hired: this.form.get(this.formHired).value,
            fired: this.form.get(this.formFired).value,
            positionId: this.selectedPosition
          }
      };
      this.dialogRef.close(employee);
    }
  }

  cancel(): void {
    this.router.navigate(['/']);
  }

  get firstName(): any { return this.form.get(this.formFirstName); }
  get lastName(): any { return this.form.get(this.formLastName); }
  get salary(): any { return this.form.get(this.formSalary); }
  get hired(): any { return this.form.get(this.formHired); }
  get fired(): any { return this.form.get(this.formFired); }
}
