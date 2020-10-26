import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { EmployeePositionViewModel } from './EmployeePositionViewModel';
import { EmployeePositionService } from '../services/employee-position.service';
import { catchError, finalize } from 'rxjs/operators';

export class EmployeePositionDataSource implements DataSource<EmployeePositionViewModel> {

    private employeeSubject = new BehaviorSubject<EmployeePositionViewModel[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public count = 0;

    public loading$ = this.loadingSubject.asObservable();

    constructor(private employeePositionService: EmployeePositionService) {}

    connect(collectionViewer: CollectionViewer): Observable<EmployeePositionViewModel[]> {
        return this.employeeSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.employeeSubject.complete();
        this.loadingSubject.complete();
    }

    loadEmployeePositions(sortDirection = 'asc', pageIndex = 0, pageSize = 3): void {
        this.employeePositionService.getEmployeePositionsCount().subscribe(count => {
            this.count = count;
        });

        this.loadingSubject.next(true);

        this.employeePositionService.getEmployeePositions(sortDirection, pageIndex, pageSize)
        .pipe(
            catchError(() => of([])),
             finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(employeePosition => this.employeeSubject.next(employeePosition));
    }
}
