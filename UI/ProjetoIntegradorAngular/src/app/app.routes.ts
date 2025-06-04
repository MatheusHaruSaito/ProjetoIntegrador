import { Routes } from '@angular/router';
import { UserListComponent } from './Admin/user-list/user-list.component';
import { HomeComponent } from './Pages/home/home.component';
import { RegisterComponent } from './Pages/register/register.component';
import { OngTicketFormComponent } from './Pages/ong-ticket-form/ong-ticket-form.component';
import {LoginComponent} from './Pages/login/login.component';
import { authGuard } from './auth.guard';

export const routes: Routes = [
    {path:'Admin-UserList',component: UserListComponent,canActivate: [authGuard]},
    {path:'',component:HomeComponent},
    {path:'Register',component:RegisterComponent},
    {path:'OngTicketForm',component:OngTicketFormComponent,},
    {path:'Login',component:LoginComponent}
];
