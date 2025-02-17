import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AboutComponent } from './info/about.component';
import { EditAboutComponent } from './edit/edit-about.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [AboutComponent, EditAboutComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
  ]
})
export class AboutModule { }
