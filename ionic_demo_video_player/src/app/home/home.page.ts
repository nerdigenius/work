import { Component } from '@angular/core';
import { StreamingMedia,StreamingAudioOptions,StreamingVideoOptions } from '@ionic-native/streaming-media/ngx';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  

  constructor(private streamingMedia:StreamingMedia) {
    
  }

  startVideo(){
    let options:StreamingVideoOptions={
      successCallback:()=>{console.log("sucess")},
      errorCallback:()=>{console.log("sucess")},
      orientation:'landscape',
      
    }
    this.streamingMedia.playVideo("https://amp.dev/static/samples/video/tokyo.mp4",options)
  }

  startAudio(){

  }

  stopAudio(){

  }




}
