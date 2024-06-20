import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginData } from '../Interfaces/login-data';
import { Observable, from, throwError } from 'rxjs';
import { error } from 'console';
import { Router } from '@angular/router';
import e from 'express';
import { LoginResponse } from '../Interfaces/login-response';
import { catchError } from 'rxjs/operators';
import { Product } from '../Interfaces/product';
import { FormProduct } from '../Interfaces/form-product';
import { ActionResponse } from '../Interfaces/action-response';
import { FormUser } from '../Interfaces/form-user';
import { User } from '../Interfaces/user';
import { GetProductResponse } from '../Interfaces/get-product-response';
import { GetUsersResponse } from '../Interfaces/get-users-response';


@Injectable({
  providedIn: 'root'
})
export class ApisService {
  urlLogin = 'https://localhost:7133/Auth/Login'

  urlTraerProductos = 'https://localhost:7133/Products/TraerProductos'
  urlInsertarProductos = 'https://localhost:7133/Products/InsertarProductos'
  urlUpdateProductos = 'https://localhost:7133/Products/UpdateProductos'
  urlDeleteProductos = 'https://localhost:7133/Products/DeleteProductos'

  urlTraerUsers = 'https://localhost:7133/User/TraerUsers'
  urlInsertarUsers = 'https://localhost:7133/User/InsertarUsers'
  urlUpdateUser = 'https://localhost:7133/User/UpdateUser'
  urlDeleteUser = 'https://localhost:7133/User/DeleteUser'

  httpOptions = {
    headers: new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('JWTToken')
    })
  };

  constructor(private router: Router , private http: HttpClient) { }

  ErrorDeAcesso : LoginResponse = { rol : 0, token : ""}
  ErrorDePermiso : LoginResponse = { rol : 0, token : "No Tienes Acceso a esta funcion"}

  ErrorTraerProductos : GetProductResponse = {success : false, message : '', result: []}
  ErrorTraerUsers : GetUsersResponse = {success : false, message : '', result: []}
  ErrorAction : ActionResponse = {success : false, message : '', result: ''}
  

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
  
  ErrorGetPorduct(error: HttpErrorResponse) : Observable <GetProductResponse>{
    if (error.status == 400 || error.status == 401) {
      this.router.navigate(['..'])
    }
    return throwError(() => this.ErrorTraerProductos)
  }

  ErrorGetUser(error: HttpErrorResponse) : Observable <GetUsersResponse>{
    if (error.status == 400 || error.status == 401) {
      this.router.navigate(['..'])
    }
    return throwError(() => this.ErrorTraerUsers)
  }
  
  ErrorSentAction(error: HttpErrorResponse) : Observable <ActionResponse>{
    if (error.status == 400 || error.status == 401) {
      this.router.navigate(['..'])
    }
    return throwError(() => this.ErrorAction)
  }


  // Apis de producto
  TraerProductos(data: FormProduct): Observable<GetProductResponse>{
    
    return this.http.post<GetProductResponse>(this.urlTraerProductos,data,this.httpOptions).pipe(catchError(this.ErrorGetPorduct.bind(this)))
  }

  InsertarProductos(data: Product): Observable<ActionResponse>{
    
    return this.http.post<ActionResponse>(this.urlInsertarProductos,data,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  UpdateProductos(data: Product): Observable<ActionResponse>{
    
    return this.http.post<ActionResponse>(this.urlUpdateProductos,data,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  DeleteProductos(data: number): Observable<ActionResponse>{
    let dataString = data.toString()
    return this.http.post<ActionResponse>(this.urlDeleteProductos +'?ID='+dataString,data,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }


  //Apis de Usuarios
  TraerUsers(data: FormUser): Observable<GetUsersResponse>{
    
    return this.http.post<GetUsersResponse>(this.urlTraerUsers,data,this.httpOptions).pipe(catchError(this.ErrorGetUser.bind(this)))
  }

  InsertarUsers(data: User): Observable<ActionResponse>{
    
    return this.http.post<ActionResponse>(this.urlInsertarUsers,data,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  UpdateUser(data: User): Observable<ActionResponse>{
    
    return this.http.post<ActionResponse>(this.urlUpdateUser,data,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  DeleteUser(data: number): Observable<ActionResponse>{
    let dataString = data.toString()
    return this.http.post<ActionResponse>(this.urlDeleteUser+'?ID='+dataString,data,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }



}
