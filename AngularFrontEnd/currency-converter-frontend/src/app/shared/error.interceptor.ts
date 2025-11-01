
import { HttpInterceptorFn } from '@angular/common/http';
import { tap } from 'rxjs/operators';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(tap({ error: err => console.error('API error', err) }));
};
