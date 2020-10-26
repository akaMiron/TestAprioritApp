import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { EmployeePositionViewModel } from '../models/EmployeePositionViewModel';

@Injectable({
  providedIn: 'root'
})
export class EmployeePositionService {

  myAppUrl: string;
  myApiUrl: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/EmployeePositions/';
  }

  getEmployeePositions(sortOrder = 'asc', pageNumber = 0, pageSize = 3): Observable<EmployeePositionViewModel[]> {
    return this.http.get<EmployeePositionViewModel[]>(this.myAppUrl + this.myApiUrl, { params: new HttpParams()
      .set('sortOrder', sortOrder)
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString())
    })
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getEmployeePositionsCount(): Observable<number> {
    return this.http.get<number>(this.myAppUrl + this.myApiUrl + 'getCount')
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  saveEmployeePosition(employeePosition: EmployeePositionViewModel): Observable<EmployeePositionViewModel> {
    return this.http.post<EmployeePositionViewModel>(this.myAppUrl + this.myApiUrl, JSON.stringify(employeePosition), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
}

  errorHandler(error): Observable<never> {
    let errorMessage = '';

    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);

    return throwError(errorMessage);
  }
}
