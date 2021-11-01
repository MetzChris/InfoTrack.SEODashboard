import React, { Component } from 'react';
import { CircularProgress } from '@mui/material';
import Paper from '@material-ui/core/Paper';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import {
    Chart,
    BarSeries,
    Title,
    ArgumentAxis,
    ValueAxis,
    Tooltip
} from '@devexpress/dx-react-chart-material-ui';
import Grid from '@mui/material/Grid';
import { EventTracker } from '@devexpress/dx-react-chart';

export class Dashboard extends Component {
    static displayName = Dashboard.name;

    constructor(props) {
        super(props);
        this.state = { seoData: [], rankData: [], targetItem: undefined, keyword: "efiling integration", url: "infotrack.com", loading: true };
        this.changeTargetItem = targetItem => {
            this.setState({ targetItem });
        }
        this.searchBtnClick = (e) => {
            this.populateSEOData();
        }
        this.keywordOnChange = (e) => {
            this.setState({keyword: e.target.value})
        }
        this.urlOnChange = (e) => {
            this.setState({ url: e.target.value })
        }
    }

    componentDidMount() {
        this.populateSEOData();
    }

    render() {
        const seoResults = this.state.seoData;
        const rankResults = this.state.rankData;
        const targetItem = this.state.targetItem;
        const keyword = this.state.keyword;
        const url = this.state.url;
        return (
            <div>
                <h1>Dashboard</h1>
                <p>Welcome to your SEO Dashboard!  Here you can see many important statistics about your websites and how they are performing in the Google search engine:</p>
                <Grid container spacing={1} className={"searchFilterMenu"}>
                    <Grid item xs={2}>
                        <TextField id="txtKeyword" label="Search Keyword" variant="outlined" className={"infoTrackInput"} value={keyword} onChange={this.keywordOnChange} />
                    </Grid>
                    <Grid item xs={2}>
                        <TextField id="txtURL" label="URL to Rank" variant="outlined" className={"infoTrackInput"} value={url} onChange={this.urlOnChange} />
                    </Grid>
                    <Grid item xs={2}>
                        <Button variant="contained" className={"infoTrackInput"} onClick={this.searchBtnClick}>Search</Button>
                    </Grid>
                </Grid>
                {this.state.loading ? <CircularProgress /> :
                    <Paper>
                        <Grid container spacing={2}>
                            <Grid item xs={3}>
                                {seoResults.map((_seoData, index) => (
                                    <Paper style={_seoData.url === url ? { backgroundColor: "orange", margin: 5 } : { backgroundColor: "white", margin: 5 }} key={index} elevation={6}>
                                        <b>URL: </b> {_seoData.url}<br />
                                        <b>Rank: </b>{_seoData.rank}<br />
                                    </Paper>
                                ))}
                            </Grid>
                            <Grid item xs={9}>
                                <Chart
                                    data={rankResults}
                                    rotated
                                >
                                    <ArgumentAxis />
                                    <ValueAxis />

                                    <BarSeries
                                        valueField="rankRating"
                                        argumentField="url"
                                    />
                                    <Title text="Google Search Rank Rating (Calculated from Rank and Number of Appearances)" />
                                    <EventTracker />
                                    <Tooltip targetItem={targetItem} onTargetItemChange={this.changeTargetItem} />
                                </Chart>
                            </Grid>
                        </Grid>
                    </Paper>
                }
            </div>
        );
    }

    async populateSEOData() {
        const response = await fetch(`dashboard?keyword=${this.state.keyword}`, {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }});
        if (response !== undefined && response.headers.get("content-type").indexOf("application/json") !== -1) {
            const data = await response.json();
            this.setState({ seoData: data.seoData, rankData: data.rankData, loading: false });
        }
    }
}

export default Dashboard;