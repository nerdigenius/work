import { Component } from '@angular/core';
import { VideoPlayer,VideoOptions } from '@ionic-native/video-player/ngx';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  videoOptions:VideoOptions;
  videoUrl:string;

  constructor(public navCtrl:NavController, private videoPlayer:VideoPlayer) {
    
  }

  async playVideo(){
    try {
      this.videoOptions={
        volume:0.7        
      }
      this.videoUrl="http://techslides.com/demos/sample-video/small.mp4"
      this.videoPlayer.play(this.videoUrl,this.videoOptions)
    } catch (error) {

      console.error(error);
      
    }
  }



}
