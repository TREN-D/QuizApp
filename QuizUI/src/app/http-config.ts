import { HttpHeaders } from '@angular/common/http';

export const PATCH_CONTENT_TYPE = new HttpHeaders({
  'Content-Type': 'application/json-patch+json'
});
