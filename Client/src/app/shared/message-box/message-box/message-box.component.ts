import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MessageBoxService, MessagePayload } from '../../../services/message-box.service';

@Component({
  selector: 'app-message-box',
  templateUrl: './message-box.component.html',
  styleUrls: ['./message-box.component.css']
})
export class MessageBoxComponent implements OnInit, OnDestroy {
  public Payload?: MessagePayload;
  private _sub!: Subscription;

  constructor(private _messageBoxService: MessageBoxService) {}

  public ngOnInit(): void {
    this._sub = this._messageBoxService.Messages$.subscribe((payload: MessagePayload) => {
      this.Payload = payload;
      setTimeout(() => (this.Payload = undefined), 5000);
    });
  }

  public ngOnDestroy(): void {
    this._sub.unsubscribe();
  }
}
