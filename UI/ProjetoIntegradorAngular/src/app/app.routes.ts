import { Routes } from '@angular/router';
import { UserListComponent } from './Admin/user-list/user-list.component';
import { HomeComponent } from './Pages/home/home.component';

export const routes: Routes = [
    {path:'Admin-UserList',component: UserListComponent},
    {path:'',component:HomeComponent}
];
