import { MatPaginator } from '@angular/material/paginator';

export const getPaginatedUrl = (paginator: MatPaginator): string => {
  return `?pageIndex=${paginator.pageIndex}&pageSize=${paginator.pageSize}`;
};
