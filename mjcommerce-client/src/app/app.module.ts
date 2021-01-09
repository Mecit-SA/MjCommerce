import { LoginService } from './Auth/services/login-service.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AdminPanelLoginComponent } from './Auth/admin-panel-login/admin-panel-login.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent, AdminPanelLoginComponent],
  imports: [BrowserModule, HttpClientModule],
  providers: [LoginService],
  bootstrap: [AppComponent],
})
export class AppModule {}
