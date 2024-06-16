# ProductosElHierro
Este es un repositorio que contiene el script de creacion de BD ademas de las APIs para el Backend de una aplicacon web de gestion de productos

# Instalacion del proyecto
Primero debe ir a su SQL server Manager studio y correr el script de creacion de la base de datos del programa.

Una vez hecho esto vaya a Visual Studio o su IDE de preferencia y abra el projecto.

Una vez el proyecto este abierto vaya a el archivo llamado: "appsettings.json" y en donde dice "ConnectionString" cambie la parte que dice "HENRYHLABTOP" a el nombre del servido en el se ejecuto el script de creacion de la base de datos.

Una vez ya el connection string eeste debidamente formateado, corra el programa, esto le deberia abrir Swagger con las Api 

# Logueo para las apis
Antes de usar las demas apis es necesario loguearse, actualmente hay 2 usuarios con roles distintos: 
 - Hermes
 - Whisman
   
Por motivos de prueba tienen los dos la contraseña 12345

Para poder loguarese se abre el primer api dentro de la seccion de "Auth", el cual tiene de ruta /Auth/Login (Si se desea usar dentro de postman u otro software favor usar https://localhost:[localhost]/Auth/Login, siendo el segundo local host el puesto en el que se esta corriendo, el cual puede verificar por medio del url en la parte superior del browser). 

Entro de esta va a dar click en "try it out" y va a reemplazar las palabras "string" por las respectivas credenciales del usuario con el que desea ingresar. 

Luego de esto va a darle al boton inferior del cuadro de texto que dice Execute y esto debe resultar en una salida de texto plano, el cual es el token a lugar.

Una vez se tenga este token se pueden utilizar las demas Apis. Para esto hay que loguearse, lo cual se hacer al darle click al candado de la parte superior y escribiendo en la barra de texto "Bearer" [espacio] [JWT Token] (Favor reemplazar las palabras entre corchetes por las entradas correspondientes)

# Apis de Auth

Aparte de la primera (La de ruta /Auth/Login mencionada anteriormente), tambien se hace uso de una segunda api con el objetivo de verificar que se token se haya ingresado correctamente y el funcionamiento del mismo sea valido.

Este es el api de ruta /Auth/Test (https://localhost:[localhost]:7133/Auth/Test) el cual si esta logueado con Whisman debe dar el Error 403, si no esta logueado el Error 401 y si esta logueado con Hermes deberia resultar en una serie de datos del rol y el nombre de ususario de Hermes, ademas de un conjunto de datos realcionados con lo logueo (Esto es distinto a los datos de usuario de Hermes que veremos mas adelante)

# Apis de Product

  1. El primer Api (/Products/TraerProductos - https://localhost:[localhost]/Products/TraerProductos) de esta seccion es un tipo post que requiere que se le de una sere de datos sobre el producto o los producto que se desea buscar, es importante destacar que no hay destacar o enfocarse en todos, ya que eestos tienen valores base con los cuales no se hace un filtrado por dicho campo (asume cualquier valor) de manera que la busqueda es mas personalizada y flexibe, ya que se puede buscar con el mismo metodo de manera indibidual por cualquier campo, aqui esta el json con los valores base, de este ser copiado y pegado va a traer todos los productos:
     {
      "idProducto": 0,
      "nombre": "",
      "descripcion": "",
      "precio1": 0,
      "precio2": 999999999.99,
      "stock1": 0,
      "stock2": 999999,
      "fechaDeCreacion1": "1999-12-31",
      "fechaDeAct1": "1999-12-31",
      "fechaDeCreacion2": "1999-12-31",
      "fechaDeAct2": "1999-12-31"
    }
  2. El segundo (/Products/InsertarProductos - https://localhost:[localhost]/Products/InsertarProductos) esta hecho para poder insertar a la base de datos los nuevos productos, el mismo solo requiere que se le suministre los primeros 4 campos ya que los otros no son tomados en cuenta, por lo cual se pueden dejar basios o como en el ejemplo, se realizo de esta forma para poder aprovechar la clase objeto que ya se debio haber creado para los procesos del sistema y porque es mas intuitivo que se le pase un producto y se cree un producto, aqui un ejemplo del Json que se puede utilizar:
    {
      "idProducto": 130,
      "nombre": "Destornillador",
      "descripcion": "Destornillador muy bueno",
      "precio": 100,
      "stock": 1000,
      "fechaDeCreacion": "2024-06-16T01:46:38.921Z",
      "fechaDeAct": "2024-06-16T01:46:38.921Z",
      "activo": true
    }
  3. El tercero (/Products/UpdateProductos - https://localhost:[localhost]/Products/UpdateProductos) se usa para actualizar cualquier producto existente, este tambien recibe un producto por las mismas razones del api anterior, aqui un ejemplo para el uso de esta
    {
      "idProducto": 130,
      "nombre": "Destornillador",
      "descripcion": "Destornillador muy bueno",
      "precio": 100,
      "stock": 1050,
      "fechaDeCreacion": "2024-06-16T01:46:38.921Z",
      "fechaDeAct": "2024-06-16T01:46:38.921Z",
      "activo": true
    }
  4. El ultimo o cuarto api (/Products/DeleteProductos - https://localhost:[localhost]/Products/DeleteProductos) se usa para la eliminacion del producto, no obstnate este hace uso del atrivuto activo de del producto y en vez de eliminarlo lo desactiva, lo que ocaciona que no pueda aparecer en la busqueda, esto se hace porque pensando a futuro y en la expansion del mismo software y base dedatos, se va a necesitar haceer refierencias de por ejemplo, compras de un producto, los cuales deben estar debidamente referenciados entro de la base de datos, por lo cual, aunque estos sea funcionalmente eliminados, todavia deben estar ahi con el objetivo de integritdad de la data de la BD, por lo cual, al desactivar el producto, ya no aparecera en las busquedad de productos. Ejemplo: ingrese 130 en la barra de texto

     Nota: recuerde precionar ejecutar para que las Apis se puedan correr

# Apis de User

  1. El Primero de estas Apis (/User/TraerUsers - https://localhost:[localhost]/Products/TraerUsers) funciona con el mismo principio que el primero de la seccion anterior, pero con los datos de usuarios, en este sentido, se presenta el ejemplo con los datos base del api, el cual si es pegado, va a devolver a todos los usuarios
    {
      "idUsuarios": 0,
      "nombreUsuario": "string",
      "contra": "string",
      "email": "string",
      "rol": 0
    }
  2. La segunda de estas Apis (/User/InsertUsers - https://localhost:[localhost]/Products/InsertUsers) es para poder insertar usuarios, esta al igual que las anteriores tambien requiere un Json con los datos del usuario que se desea agregar, aunque cabe destacar que este no hace uso del campo "Activo" ya que cuando se ingresa un usuario esta por defualt activo, siendo que este metodo utiliza la clase usuario, por la misma razon que la segunda y tercera de la seccion anterior utilizan un producto. Aqui un ejemplo del Json a suar en esta Api. Por ultimo es necesario destarcar que la contraseña a utilizar debe contener mayusculas, minusculas, numeros y caracteres especiales para poder ser aceptada y el email tiene que contener un "@" para poder verificar que si es un email, mientras que el nombre no puede contener un "@" porque si no se entiende como un email, por ultimo los roles son: 1-administrado, 2-usuario
    {
      "idUsuarios": 2223822,
      "nombreUsuario": "Maria",
      "contra": "Mar!4",
      "email": "Maria@Company.com.do",
      "rol": 1,
      "activo": true
    }
  3. El tercero (/User/UpdateUsers - https://localhost:[localhost]/Products/UpdateUsers) es uno de actualizacion de los usuarios, este tiene las mismas restricciones que el anterior y necesita los mismos datos, aqui un ejemplo:
    {
      "idUsuarios": 2223822,
      "nombreUsuario": "Maria",
      "contra": "Mar!4#",
      "email": "Maria@company.com.do",
      "rol": 1,
      "activo": true
    }
  4. Por ultimo tenemos el Api (/User/DeleteUsers - https://localhost:[localhost]/Products/DeleteUsers) para la eliminacion de cuentas, la cual al igual que la eliminacion de la seccion anterior, se basa en desactivar a un usuario para que no pueda ser encintrado por medio de las busquedas (las cuales tambien se usan en la autenticacion) de manera que si se trata de expandir el software la base de datos va a mantener la integridad porque aunque se despida o de desabilite una persona, las referenciad de quien es y que hizo van a seguir ahi. Ejemplo: colocar en la barra de texto 2223822

     Nota: recuerde precionar ejecutar para que las Apis se puedan correr

