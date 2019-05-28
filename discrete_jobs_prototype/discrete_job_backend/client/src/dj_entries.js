import React,{Component} from "react"


class DjEntries extends Component {

    createTasks(items){
        return <li key={items.key}>{items.text}</li>
    }

    render(){
        var todoEntries=this.props.entries;
        var listItems = todoEntries.map(this.createTasks);

        return(
            <ul className="thelist">
                {listItems}
            </ul>
        )
    }

}

export default DjEntries; 