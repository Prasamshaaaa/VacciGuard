// src/app/shared/response.model.ts
export class Response<T> {
  Results: T | null = null;
  Status: string = '';
  ErrorMessage: string = '';
}
