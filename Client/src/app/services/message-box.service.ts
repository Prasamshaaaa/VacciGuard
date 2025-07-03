import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { MessageBoxStatus } from '../shared/shared.enums';

/** Defines the payload structure for messages */
export interface MessagePayload {
  status: MessageBoxStatus;
  messages: string[];
}

@Injectable({
  providedIn: 'root'  // makes this service globally available
})
export class MessageBoxService {
  // Internal subject that holds and emits message payloads
  private _subject = new Subject<MessagePayload>();

  /** Observable stream for components to subscribe and receive messages */
  public get Messages$(): Observable<MessagePayload> {
    return this._subject.asObservable();
  }

  /**
   * Emit a new message payload.
   * @param status the type/severity of the message (success, error, etc.)
   * @param messages one or more text lines to display
   */
  public ShowMessage(status: MessageBoxStatus, messages: string[]): void {
    this._subject.next({ status, messages });
  }
}
