# PARA CREAR AUTM�TICAMENTE DESDE ENTITYFRAMEWORK
#! INSTALAR EFCORE,EFCORE SqlServer, EFCORE TOOLS

#LUEGO VAMOS TOOLS, NUGGET MANAGER, NUGGET CONSOLE Y PEGAMOS LO SIGUIENTE
Scaffold-DbContext "Server=DESKTOP-RN0JGF1\SQLEXPRESS;Database=BASE_ERP;Integrated Security=true;TrustServerCertificate=True;User=sa;Password=sa" Microsoft.EntityFrameworkCore.SqlServer -o Models -f

# CONFIGURACION DESDE GIT
"ConnectionDefault": " Server=DERECK\\SQLEXPRESS; Database=BASE_ERP; Integrated Security=true; TrustServerCertificate=true"

# CONFIGURACION DE DMS GIT
"DefaultConnection": "Server=localhost\\MSSQLSERVER01;Database=BASE_ERP;Integrated Security=true;TrustServerCertificate=True;User=sa;Password=Ucg.2023"

# CONFIGURACION LAPTOP DE CASA
"DefaultConnection": "Server=DESKTOP-RN0JGF1\SQLEXPRESS;Database=BASE_ERP;Integrated Security=true;TrustServerCertificate=True;User=sa;Password=sa"