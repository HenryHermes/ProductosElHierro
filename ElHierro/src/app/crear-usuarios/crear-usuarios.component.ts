import { Component } from '@angular/core';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';
import { FormsModule } from '@angular/forms';
import { ApisService } from '../Services/apis.service';
import { User } from '../Interfaces/user';
import { ActionResponse } from '../Interfaces/action-response';

@Component({
  selector: 'app-crear-usuarios',
  standalone: true,
  imports: [NavMenuComponent,FormsModule],
  templateUrl: './crear-usuarios.component.html',
  styleUrl: './crear-usuarios.component.css'
})
export class CrearUsuariosComponent {
  constructor(private Api: ApisService){}

  Usuario : User = {idUsuarios : 0, nombreUsuario : '', email : '', contra:'',rol:0, activo: true}

  Ejecucion = ''

  contra1 = ''
  contra2 = ''

  coincidencia = ''
  BoolCoinciden = false

  CapturarContra1(str :string){
    this.contra1 = str
    this.coinciden()
  }
  CapturarContra2(str :string){
    this.contra2 = str
    this.coinciden()
  }

  coinciden(){
    if (this.contra1==this.contra2) {
      this.coincidencia=''
      this.BoolCoinciden=true
    }else{
      this.coincidencia='No coinciden las contraseñas'
      this.BoolCoinciden=false
    }
  }

  CreateOrUpdate(Form: any){
    
    if(this.BoolCoinciden==true){
      this.Usuario.idUsuarios = Form.idUsuarios
    this.Usuario.nombreUsuario = Form.nombreUsuario
    this.Usuario.email = Form.email
    this.Usuario.contra = Form.contra
    this.Usuario.rol = Form.rol

    if(Form.Operacion == "1"){
      this.Api.UpdateUser(this.Usuario).subscribe((Result:ActionResponse)=>{
        if(Result.success == true){
          this.Ejecucion='Se ha realizado exitosamente'
        }else{
          this.Ejecucion=''
        }
      })
    }else{
      this.Api.InsertarUsers(this.Usuario).subscribe((Result:ActionResponse)=>{
        if(Result.success == true){
          this.Ejecucion='Se ha realizado exitosamente'
        }else{
          this.Ejecucion=''
        }
      })
    }
    }

  }
}
