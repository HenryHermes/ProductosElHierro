import { Component,Input } from '@angular/core';
import { User } from '../Interfaces/user';
import { ApisService } from '../Services/apis.service';

@Component({
  selector: 'app-user-item',
  standalone: true,
  imports: [],
  templateUrl: './user-item.component.html',
  styleUrl: './user-item.component.css'
})
export class UserItemComponent {
  @Input() Usuario!: User;

  constructor (private Api: ApisService){}

  

  cambirarRol(num: number){
    let rol = 'User'
    
    if (num == 1) {
      rol = 'Admin'
    }

    return rol
  }

  BorrarUsuario(){
    this.Api.DeleteUser(this.Usuario.idUsuarios).subscribe(() =>{
      this.Usuario.nombreUsuario= '#####'
      this.Usuario.idUsuarios= 0
      this.Usuario.email = '#####'
      this.cambirarRol(this.Usuario.rol)
    })
  }
  
}
