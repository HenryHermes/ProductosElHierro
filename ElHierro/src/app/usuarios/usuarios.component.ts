import { Component, OnInit } from '@angular/core';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';
import { ApisService } from '../Services/apis.service';
import { FormUser } from '../Interfaces/form-user';
import { User } from '../Interfaces/user';
import { GetUsersResponse } from '../Interfaces/get-users-response';
import { UserItemComponent } from '../user-item/user-item.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [NavMenuComponent,UserItemComponent,CommonModule,FormsModule],
  templateUrl: './usuarios.component.html',
  styleUrl: './usuarios.component.css'
})
export class UsuariosComponent implements OnInit {
  constructor (private Api: ApisService){}

  formDeUserInicial : FormUser = {idUsuario : 0, nombreUsuario : '', contra: '', email:'',rol:0}

  

  ngOnInit(): void {   
    this.GetUsers(this.formDeUserInicial) 
  }

  ListUsers: User[] = []

  GetUsers(Form : FormUser){
    Form.contra = ''
    console.log(Form)
    this.Api.TraerUsers(Form).subscribe((Result : GetUsersResponse)=>{
      this.ListUsers = Result.result
      console.log(this.ListUsers)
    })
  }


}
