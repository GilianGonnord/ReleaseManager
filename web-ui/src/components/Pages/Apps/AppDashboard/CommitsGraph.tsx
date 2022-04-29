import { Card, CardContent, Typography } from '@mui/material';
import moment from 'moment';
import { Bar, BarChart, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts';

type Commit = {
    name: string;
}

const monthDates = [];

const now = moment();
const date = moment().startOf('month');

while (date.isSame(now, 'month')) {
    monthDates.push(date.clone());
    date.add(1, 'day')
}

const commits: Commit[] = monthDates.map(monthDate => ({
    name: monthDate.format("L"),
    commits: Math.floor(Math.random() * 100)
}));

export const CommitsGraph = () => {
    return (
        <Card>
            <CardContent>
                <Typography gutterBottom variant="subtitle2" component="div" textAlign="center">
                    Commits this month
                </Typography>
                <ResponsiveContainer height={300}>
                    <BarChart height={300} data={commits} >
                        <XAxis dataKey="name" interval="preserveStartEnd" ticks={[commits[0].name, commits[commits.length - 1].name]} />
                        <YAxis />
                        <Tooltip wrapperStyle={{ color: '#121212' }} />
                        <Bar dataKey="commits" fill="#8884d8" />
                    </BarChart>
                </ResponsiveContainer>
            </CardContent>
        </Card>
    )
}