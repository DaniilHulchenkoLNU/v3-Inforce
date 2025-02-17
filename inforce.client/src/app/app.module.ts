import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from './modules/shared/shared.module';
//import { AboutComponent } from './modules/about/about.component';
import { JwtModule } from '@auth0/angular-jwt';
import { AboutModule } from './modules/about/about.module';


export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    SharedModule,
    AboutModule ,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:7234'],
        disallowedRoutes: ['localhost:7234/api/auth'],
      },
    }),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
