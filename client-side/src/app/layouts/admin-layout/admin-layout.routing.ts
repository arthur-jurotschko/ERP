import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { TypographyComponent } from '../../typography/typography.component';
import { IconsComponent } from '../../icons/icons.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
// import { ListaLigacoesComponent } from 'app/recepcao/ligacoes/lista-ligacoes/lista-ligacoes.component';
// import { AdicionarLigacoesComponent } from 'app/recepcao/ligacoes/adicionar-ligacoes/adicionar-ligacoes.component';
// import { MinhasLigacoesComponent } from 'app/recepcao/ligacoes/minhas-ligacoes/minhas-ligacoes.component';
import { LoginComponent } from 'app/user-profile/login/login.component';
import { ConfirmacaoemailComponent } from 'app/user-profile/confirmacaoemail/confirmacaoemail.component';
import { ForgotpasswordComponent } from 'app/user-profile/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from 'app/user-profile/resetpassword/resetpassword.component';
import { AuthService } from 'app/services/auth.service';

import { MeusClientesComponent } from 'app/comercial/meus-clientes/meus-clientes.component';
import { AdicionarClientesComponent } from 'app/comercial/adicionar-clientes/adicionar-clientes.component';
import { EditarClientesComponent } from 'app/comercial/editar-clientes/editar-clientes.component';
import { DetalhesClientesComponent } from 'app/comercial/detalhes-clientes/detalhes-clientes.component';
import { ExcluirClientesComponent } from 'app/comercial/excluir-clientes/excluir-clientes.component';
import { DashboardClientesComponent } from 'app/comercial/meus-clientes/dashboard-clientes/dashboard-clientes.component';
import { SignupComponent } from 'app/user-profile/signup/signup.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'table-list',     component: TableListComponent },
    { path: 'typography',     component: TypographyComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'maps',           component: MapsComponent },
    { path: 'notifications',  component: NotificationsComponent },
    { path: 'clientes/meus-clientes',  component: MeusClientesComponent },
    { path: 'clientes/adicionar-cliente',  component: AdicionarClientesComponent },
    { path: 'clientes/editar-cliente/:id',  component: EditarClientesComponent },
    { path: 'clientes/detalhes-cliente/:id',  component: DetalhesClientesComponent },
    { path: 'clientes/excluir-cliente/:id',  component: ExcluirClientesComponent },
    { path: 'clientes/dashboard-clientes',  component: DashboardClientesComponent },

    { path: 'login', component: LoginComponent },
    { path: 'confirmacaoemail', component: ConfirmacaoemailComponent },
    { path: 'forgotpassword', component: ForgotpasswordComponent },
    { path: 'resetpassword', component: ResetpasswordComponent},
    { path: 'signup', component: SignupComponent},

                  
    // { path: 'minhas-ligacoes', canActivate: [AuthService], data: [{ claim: {nome:"Administrador", valor:"Gravar"} }], component: MinhasLigacoesComponent },
            
   
];
