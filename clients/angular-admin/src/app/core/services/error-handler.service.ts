import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  constructor(
    private router: Router,
    private authService: AuthService,
    private messageService: MessageService
  ) {}

  handleError(error: HttpErrorResponse): void {
    let message = 'An unexpected error occurred';

    if (error.status === 401) {
      this.authService.logout();
      this.router.navigate(['/login']);
      message = 'Session expired. Please login again.';
    } else if (error.status === 403) {
      message = 'You do not have permission to perform this action';
    } else if (error.status === 404) {
      message = 'Resource not found';
    } else if (error.status === 429) {
      message = 'Too many requests. Please try again later.';
    } else if (error.error?.message) {
      message = error.error.message;
    }

    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: message
    });
  }
}
