import { Routes } from '@angular/router';
import { UserListComponent } from './Pages/Admin/user-list/user-list.component';
import { HomeComponent } from './Pages/home/home.component';
import { RegisterComponent } from './Pages/register/register.component';
import { OngTicketFormComponent } from './Pages/ong-ticket-form/ong-ticket-form.component';
import { TicketListComponent } from './Pages/Admin/ticket-list/ticket-list.component';
import { ProfileComponent } from './Pages/profile/profile.component';
import { FeedbackComponent } from './Pages/feedback/feedback.component';

import { LoginComponent } from './Pages/login/login.component';
import { authGuard } from './auth.guard';
import { AboutUsComponent } from './Pages/about-us/about-us.component';       

export const routes: Routes = [
    {path:'Admin-UserList',component: UserListComponent,canActivate: [authGuard]},
    {path:'Admin-TicketList',component:TicketListComponent},
    {path:'',component:HomeComponent},
    {path:'Register',component:RegisterComponent},
    {path:'OngTicketForm',component:OngTicketFormComponent,},
    {path:'Login',component:LoginComponent},
    {path:'AboutUs',component:AboutUsComponent},
    {path:'Profile',component:ProfileComponent},
    {path:'Feedback', component: FeedbackComponent},
];
