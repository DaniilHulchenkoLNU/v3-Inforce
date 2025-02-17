import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './modules/shared/guards/auth.guard';
import { EditAboutComponent } from './modules/about/edit/edit-about.component';
import { AdminGuard } from './modules/shared/guards/admin.guard';
import { AboutComponent } from './modules/about/info/about.component';

const routes: Routes = [
  {
    path: '',
    component: AboutComponent, pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
  },
  { path: 'urls', loadChildren: () => import('./modules/url/url.module').then(m => m.UrlModule), canActivate: [AuthGuard] },
  { path: 'about', component: AboutComponent },
  { path: 'about/edit', component: EditAboutComponent, canActivate: [AdminGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
