import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Position } from '../models/position';

@Injectable({
  providedIn: 'root'
})
export class PositionService {
  myAppUrl: string;
  myApiUrl: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };
  constructor(private http: HttpClient) {
      this.myAppUrl = environment.appUrl;
      this.myApiUrl = 'api/Positions/';
  }

  getPositions(): Observable<Position[]> {
    return this.http.get<Position[]>(this.myAppUrl + this.myApiUrl)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getPosition(positionId: number): Observable<Position> {
      return this.http.get<Position>(this.myAppUrl + this.myApiUrl + positionId)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  savePosition(position): Observable<Position> {
      return this.http.post<Position>(this.myAppUrl + this.myApiUrl, JSON.stringify(position), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  updatePosition(positionId: number, position): Observable<Position> {
      return this.http.put<Position>(this.myAppUrl + this.myApiUrl + positionId, JSON.stringify(position), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.errorHandler)
      );
  }

  deletePosition(positionId: number): Observable<Position> {
      return this.http.delete<Position>(this.myAppUrl + this.myApiUrl + positionId)
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
