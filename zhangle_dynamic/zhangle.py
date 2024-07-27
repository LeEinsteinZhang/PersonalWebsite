from flask import Flask, render_template, request, redirect, url_for, session, send_from_directory
import json
import os

app = Flask(__name__)
app.secret_key = '233'
app.url_map.strict_slashes = False

default_lang = 'en'

def load_content(page, lang):
    content_path = os.path.join('content', page, f'{lang}.json')
    base_path = os.path.join('content', 'base', f'{lang}.json')
    
    if not os.path.exists(content_path) or not os.path.exists(base_path):
        return None

    with open(content_path, 'r', encoding='utf-8') as file:
        content = json.load(file)
    
    with open(base_path, 'r', encoding='utf-8') as file:
        footer_content = json.load(file)
    
    content.update(footer_content)
    return content

@app.route('/')
def home():
    if 'lang' not in session:
        session['lang'] = default_lang
    lang = session['lang']
    content = load_content('index', lang)
    session['prev_url'] = '/'
    return render_template('index.html', content=content, lang=lang, current_page='index', default_lang=default_lang)

@app.route('/switch_lang/<lang>')
def switch_lang(lang):
    session['lang'] = lang
    next_url = request.args.get('next', request.referrer or url_for('home'))
    return redirect(next_url)

@app.route('/resume/<filename>')
def download_resume(filename):
    return send_from_directory(os.path.join(app.root_path, 'static/resume'), filename)

@app.route('/<experience>')
def experiences(experience):
    if 'lang' not in session:
        session['lang'] = default_lang
    lang = session['lang']
    content = load_content(f'experiences/{experience}', lang)
    if content is None:
        return page_not_found(None)
    session['prev_url'] = request.path
    return render_template('experience.html', content=content, lang=lang, current_page=experience, default_lang=default_lang)

@app.route('/favicon.ico')
def favicon():
    return send_from_directory(os.path.join(app.root_path, 'static/icons'), 'favicon.ico', mimetype='image/vnd.microsoft.icon')

@app.route('/sitemap.xml')
def sitemap():
    return send_from_directory(os.path.join(app.root_path, 'static'), 'sitemap.xml')

@app.errorhandler(404)
def page_not_found(e):
    if 'lang' not in session:
        session['lang'] = default_lang
    lang = session['lang']
    prev_url = session.get('prev_url', '/')
    return render_template('404.html', lang=lang, prev_url=prev_url), 404

@app.route('/<path:path>')
def catch_all(path):
    return page_not_found(None)

if __name__ == '__main__':
    app.run()
