const React = require('react')
class Timer extends React.Component {

    constructor(props){
        super(props)
        this.state = {
            time: 0,
            start: 0,
            isOn: false,
            
          }
        this.startTimer = this.startTimer.bind(this)
        this.stopTimer = this.stopTimer.bind(this)
        this.resetTimer = this.resetTimer.bind(this)
      }
    
    startTimer() {

      if(this.state.isOn===false)
      {
        this.setState({
          time: this.state.time,
          start: Date.now() -this.state.time,
          isOn: true,
        })

        this.props.updateTimerStart(true);

        
  
  
        this.timer = setInterval(() => this.setState({
          time: Date.now() - this.state.start
        }), 1000)
        
          console.log(Date.now())

      }

      
      }
      stopTimer() {
        this.setState({isOn: false})
        this.props.timeElapsed(Math.round((this.state.time)/1000))
        this.props.updateViewList("show")        
        clearInterval(this.timer)
        console.log("stop")
      }
      resetTimer() {
        if(this.state.isOn===false)
        {

          this.setState({time: 0})
          this.props.updateViewList("hide")          
          console.log("reset")
          this.props.updateTimerStart(false);

        }
        
      }
      

  render() {

    
     return (
       <div>
         
        <h3>timer: {Math.round((this.state.time)/1000)} </h3>
        <button onClick={this.startTimer}>start</button>
        <button onClick={this.stopTimer}>stop</button>
        <button onClick={this.resetTimer}>reset</button>
       </div>
     );
  }
}
module.exports = Timer