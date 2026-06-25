import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { ErrorHandlerService } from '../error-handler.service';
import { AuthService } from '../../auth.service';

describe('ErrorHandlerService', () => {
  let service: ErrorHandlerService;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let messageServiceSpy: jasmine.SpyObj<MessageService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    authServiceSpy = jasmine.createSpyObj('AuthService', ['logout', 'getToken']);
    messageServiceSpy = jasmine.createSpyObj('MessageService', ['add']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      providers: [
        ErrorHandlerService,
        { provide: AuthService, useValue: authServiceSpy },
        { provide: MessageService, useValue: messageServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    });

    service = TestBed.inject(ErrorHandlerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should logout on 401 error', () => {
    const error = new HttpErrorResponse({ status: 401 });
    service.handleError(error);
    expect(authServiceSpy.logout).toHaveBeenCalled();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
  });

  it('should show permission error on 403', () => {
    const error = new HttpErrorResponse({ status: 403 });
    service.handleError(error);
    expect(messageServiceSpy.add).toHaveBeenCalledWith(
      jasmine.objectContaining({ severity: 'error' })
    );
  });

  it('should show rate limit error on 429', () => {
    const error = new HttpErrorResponse({ status: 429 });
    service.handleError(error);
    expect(messageServiceSpy.add).toHaveBeenCalledWith(
      jasmine.objectContaining({ severity: 'error' })
    );
  });

  it('should extract message from error body', () => {
    const error = new HttpErrorResponse({
      status: 400,
      error: { message: 'Custom error message' }
    });
    service.handleError(error);
    expect(messageServiceSpy.add).toHaveBeenCalledWith(
      jasmine.objectContaining({ detail: 'Custom error message' })
    );
  });

  it('should show not found on 404', () => {
    const error = new HttpErrorResponse({ status: 404 });
    service.handleError(error);
    expect(messageServiceSpy.add).toHaveBeenCalledWith(
      jasmine.objectContaining({ detail: 'Resource not found' })
    );
  });

  it('should show general error on unknown status', () => {
    const error = new HttpErrorResponse({ status: 0 });
    service.handleError(error);
    expect(messageServiceSpy.add).toHaveBeenCalledWith(
      jasmine.objectContaining({ detail: 'An unexpected error occurred' })
    );
  });
});
