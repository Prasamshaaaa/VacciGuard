import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
// setting up lazy loading for auth  to load the AuthModule only when a user navigates to an /auth/... route.

{
  path: 'auth',
  loadChildren: () => import('./components/auth/auth.module').then(m=>m.AuthModule)
},
{path: '', redirectTo: 'auth/login', pathMatch: 'full'},
{path: '**', redirectTo: 'auth/login'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
