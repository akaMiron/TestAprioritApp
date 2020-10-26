import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { fromEvent, merge, Observable } from 'rxjs';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { PositionService } from '../services/position.service';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeAddEditComponent } from '../employee-add-edit/employee-add-edit.component';
import { PositionAddEditComponent } from '../position-add-edit/position-add-edit.component';
import { EmployeePositionService } from '../services/employee-position.service';
import { EmployeePositionViewModel } from '../models/EmployeePositionViewModel';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { EmployeePositionDataSource } from '../models/dataSource';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent implements OnInit, AfterViewInit {

  dataSource: EmployeePositionDataSource;
  employeePosition: EmployeePositionViewModel;
  displayedColumns: string[] = ['number', 'position', 'employee', 'salary', 'hired', 'fired'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') input: ElementRef;

  constructor(
    private positionService: PositionService,
    private positionEmployeeService: EmployeePositionService,
    public dialog: MatDialog,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    // this.employeePosition = this.route.snapshot.data["course"];
    this.dataSource = new EmployeePositionDataSource(this.positionEmployeeService);
    this.dataSource.loadEmployeePositions('asc', 0, 3);
  }

  ngAfterViewInit(): void {

    // reset the paginator after sorting
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    // on sort or paginate events, load a new page
    merge(this.sort.sortChange, this.paginator.page)
    .pipe(
        tap(() => this.loadEmployeePositionPage())
    )
    .subscribe();
  }

  loadEmployeePositionPage(): void {
    this.dataSource.loadEmployeePositions(
        this.sort.direction,
        this.paginator.pageIndex,
        this.paginator.pageSize);
  }

  openEmployeeModal(): void {
    const dialogRef = this.dialog.open(EmployeeAddEditComponent, {
      width: '520px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.positionEmployeeService.saveEmployeePosition(result as EmployeePositionViewModel).subscribe((data) => {
        this.loadEmployeePositionPage();
      });
    });
  }

  openPositionModal(): void {
    const dialogRef = this.dialog.open(PositionAddEditComponent, {
      width: '520px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.positionService.savePosition(result as Position).subscribe((data) => {
      });
    });
  }
}
