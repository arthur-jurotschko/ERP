import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { TypographyComponent } from '../../typography/typography.component';
import { IconsComponent } from '../../icons/icons.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { ChartsModule } from 'ng2-charts';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';


// Sort Search
import { OrderModule } from 'ngx-order-pipe';
// import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import { MatTableModule, MatSortModule } from '@angular/material';

import { MyDatePickerModule } from 'mydatepicker';

// Pagination
import { NgxPaginationModule } from 'ngx-pagination';
import { ClientesFilter } from 'app/comercial/filters/clientes.filter';
import { Title } from '@angular/platform-browser';
import { SeoService } from 'app/services/seo.services';
import { ClientesService } from 'app/comercial/services/clientes.services';


import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UsuarioService } from 'app/user-profile/services/usuario.service';
import { LoginComponent } from 'app/user-profile/login/login.component';
import { ConfirmacaoemailComponent } from 'app/user-profile/confirmacaoemail/confirmacaoemail.component';
import { ForgotpasswordComponent } from 'app/user-profile/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from 'app/user-profile/resetpassword/resetpassword.component';
import { AuthService } from 'app/services/auth.service';
import { ErrorInterceptor } from 'app/services/error-handler.service';

import { MeusClientesComponent } from 'app/comercial/meus-clientes/meus-clientes.component';
import { AdicionarClientesComponent } from 'app/comercial/adicionar-clientes/adicionar-clientes.component';
import { EditarClientesComponent } from 'app/comercial/editar-clientes/editar-clientes.component';
import { DetalhesClientesComponent } from 'app/comercial/detalhes-clientes/detalhes-clientes.component';
import { ExcluirClientesComponent } from 'app/comercial/excluir-clientes/excluir-clientes.component';
import { DashboardClientesComponent } from 'app/comercial/meus-clientes/dashboard-clientes/dashboard-clientes.component';
import { SignupComponent } from 'app/user-profile/signup/signup.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ChartsModule,
    NgbModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-left' }),
    MyDatePickerModule,
    NgxPaginationModule,
    OrderModule,
    HttpClientModule,
    ReactiveFormsModule

  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    TableListComponent,
    TypographyComponent,
    IconsComponent,
    MapsComponent,
    NotificationsComponent,
    // **** Account **** 
    LoginComponent,
    ConfirmacaoemailComponent,
    ForgotpasswordComponent,
    ResetpasswordComponent,
    SignupComponent,
    // **** Clientes ****  
     ClientesFilter,
     MeusClientesComponent,
     AdicionarClientesComponent,
     EditarClientesComponent,
     DetalhesClientesComponent,
     ExcluirClientesComponent,
     DashboardClientesComponent
 
  ],
  providers: [
    Title,
    SeoService,
    ClientesService,
    UsuarioService,
    AuthService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ErrorInterceptor,
        multi: true
    }
],
exports: [RouterModule]
})

export class AdminLayoutModule {}
