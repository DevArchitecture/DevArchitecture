import { TestBed } from "@angular/core/testing";
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";
import { ApiService } from "../api.service";

describe("ApiService.getAllRows", () => {
  let service: ApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({ imports: [HttpClientTestingModule] });
    service = TestBed.inject(ApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it("unwraps paginated body and fetches every page", () => {
    let result: unknown[] | undefined;
    service.getAllRows("/users").subscribe((rows) => (result = rows));

    const first = httpMock.expectOne((req) => req.params.get("pageNumber") === "1");
    expect(first.request.params.get("pageSize")).toBe("100");
    first.flush({ success: true, data: [{ userId: 1 }], totalRecords: 3, pageNumber: 1 });

    const second = httpMock.expectOne((req) => req.params.get("pageNumber") === "2");
    second.flush({ success: true, data: [{ userId: 2 }], totalRecords: 3, pageNumber: 2 });

    const third = httpMock.expectOne((req) => req.params.get("pageNumber") === "3");
    third.flush({ success: true, data: [{ userId: 3 }], totalRecords: 3, pageNumber: 3 });

    expect(result).toEqual([{ userId: 1 }, { userId: 2 }, { userId: 3 }]);
  });

  it("returns bare array body with a single request", () => {
    let result: unknown[] | undefined;
    service.getAllRows("/groups").subscribe((rows) => (result = rows));

    const request = httpMock.expectOne((req) => req.params.get("pageNumber") === "1");
    request.flush([{ id: 1 }, { id: 2 }]);

    expect(result).toEqual([{ id: 1 }, { id: 2 }]);
  });

  it("stops after first page when totalRecords matches", () => {
    let result: unknown[] | undefined;
    service.getAllRows("/users").subscribe((rows) => (result = rows));

    const request = httpMock.expectOne((req) => req.params.get("pageNumber") === "1");
    request.flush({ success: true, data: [{ userId: 1 }], totalRecords: 1, pageNumber: 1 });

    expect(result).toEqual([{ userId: 1 }]);
  });

  it("stops when a wrapped page comes back empty", () => {
    let result: unknown[] | undefined;
    service.getAllRows("/users").subscribe((rows) => (result = rows));

    const first = httpMock.expectOne((req) => req.params.get("pageNumber") === "1");
    first.flush({ success: true, data: [{ userId: 1 }], totalRecords: 5, pageNumber: 1 });

    const second = httpMock.expectOne((req) => req.params.get("pageNumber") === "2");
    second.flush({ success: true, data: [], totalRecords: 5, pageNumber: 2 });

    expect(result).toEqual([{ userId: 1 }]);
  });
});
