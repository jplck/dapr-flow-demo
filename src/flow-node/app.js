const express = require('express')

const app = express()
app.use(express.json({ type: 'application/*+json' }));

const port = 8000

app.get('/dapr/subscribe', (req, res) => {
    res.json(
        [
            {
                "pubsubname": "servicebus-pubsub",
                "topic": "deviceevents",
                "route": "flowevents"
            }
        ]
    )
})

app.post("/flowevents", (req, res) => {
    console.log(req.body.data)
    res.sendStatus(200)
})

app.listen(port, () => console.log(`Node App listening on port ${port}!`));