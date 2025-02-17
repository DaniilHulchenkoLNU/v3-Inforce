import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UrlTableComponent } from './url-table/url-table.component';
import { UrlInfoComponent } from './url-info/url-info.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [UrlTableComponent, UrlInfoComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      { path: '', component: UrlTableComponent },
      { path: 'url/:shortCode', component: UrlInfoComponent }
    ])
  ]
})
export class UrlModule { }
