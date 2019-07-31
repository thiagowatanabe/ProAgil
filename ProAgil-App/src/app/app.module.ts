import { TituloComponent } from './_shared/titulo/titulo.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule,  ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TooltipModule, BsDropdownModule, ModalModule, BsDatepickerModule  } from 'ngx-bootstrap';

import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from './nav/nav.component';
import { DateTimeFormatPipe } from './_helps/DateTimeFormat.pipe';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { ContatosComponent } from './contatos/contatos.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipe,
      PalestrantesComponent,
      ContatosComponent,
      DashboardComponent,
      TituloComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      BsDatepickerModule.forRoot(),
      ToastrModule.forRoot({
         timeOut: 3000,
         preventDuplicates: true,
         progressBar: true,
         positionClass: 'toast-top-full-width'
      })
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
