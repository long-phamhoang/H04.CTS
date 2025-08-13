import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { BreadcrumbItem } from '@app/shared/models';

@Component({
  standalone: false,
  selector: 'page-header',
  templateUrl: 'page-header.component.html',
  styleUrls: ['page-header.component.less'],
})

export class PageHeaderComponent {
  @Input() title: string;
  @Input() description: string;
  @Input() breadcrumbs: BreadcrumbItem[];

  constructor(private router: Router) { }

  goToBreadcrumb(breadcrumb: BreadcrumbItem): void {
    if (!breadcrumb.routerLink) {
      return;
    }

    if (breadcrumb.navigationExtras) {
      this.router.navigate([breadcrumb.routerLink], breadcrumb.navigationExtras);
    } else {
      this.router.navigate([breadcrumb.routerLink]);
    }
  }
}
