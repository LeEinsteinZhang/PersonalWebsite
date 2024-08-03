import express from 'express';
import session from 'express-session';
import path from 'path';
import { v4 as uuidv4 } from 'uuid';
import indexRouter from './routes/index';

const app = express();

app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');
app.use(express.static(path.join(__dirname, '../public')));
app.use(session({
  secret: '65536',
  resave: false,
  saveUninitialized: true
}));

// Initialize session with default values
app.use((req, res, next) => {
  const sess: any = req.session;
  if (!sess.data) {
    sess.data = {
      session_id: uuidv4(),
      lang: 'en',
      prev_url: '/'
    };
  }
  next();
});

app.use('/', indexRouter);

// Handle 404
app.use((req, res) => {
  const sess: any = req.session;
  const lang = sess.data.lang || 'en';
  const prev_url = sess.data.prev_url || '/';
  res.status(404).render('404', { lang, prev_url });
});

app.listen(3000, () => {
  console.log('Server is running on http://localhost:3000');
});
