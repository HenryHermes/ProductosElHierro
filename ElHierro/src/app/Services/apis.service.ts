import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginData } from '../Interfaces/login-data';
import { Observable, from, throwError } from 'rxjs';
import { error } from 'console';
import e from 'express';
import { LoginResponse } from '../Interfaces/login-response';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class ApisService {
  urlLogin = 'https://localhost:7133/Auth/Login'
  constructor(private http: HttpClient) { }

  ErrorDeAcesso : LoginResponse = { success : false, message : ""}
  ErrorDePermiso : LoginResponse = { success : false, message : "No Tienes Acceso a esta funcion"}

  LoginUsers(data: LoginData): Observable<LoginResponse>{
    
    return this.http.post<LoginResponse>(this.urlLogin,data).pipe(catchError(this.TranslateError.bind(this)))
  }

  TranslateError(error: HttpErrorResponse) : Observable <LoginResponse>{
    if (error.status == 400 || error.status == 401) {
      return throwError(() => this.ErrorDeAcesso)
    }else{
      return throwError(() => this.ErrorDePermiso)
    }
  }

}
