from flask import Flask, render_template, request, redirect, url_for, session, send_from_directory
import json
import os

app = Flask(__name__)
app.secret_key = '233'
app.url_map.strict_slashes = False

default_lang = 'en'

def load_content(page, lang):
    with open(os.path.join('content', page, f'{lang}.json'), 'r', encoding='utf-8') as file:
        content = json.load(file)
    with open(os.path.join('content', 'base', f'{lang}.json'), 'r', encoding='utf-8') as file:
        footer_content = json.load(file)
    content.update(footer_content)
    return content

@app.route('/')
def home():
    if 'lang' not in session:
        session['lang'] = default_lang
    lang = session['lang']
    content = load_content('index', lang)
    return render_template('index.html', content=content, lang=lang, current_page='index', default_lang=default_lang)

@app.route('/switch_lang/<lang>')
def switch_lang(lang):
    session['lang'] = lang
    next_url = request.args.get('next', url_for('home'))
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
    return render_template('experience.html', content=content, lang=lang, current_page=experience, default_lang=default_lang)

@app.route('/favicon.ico')
def favicon():
    return send_from_directory(os.path.join(app.root_path, 'static/icons'), 'favicon.ico', mimetype='image/vnd.microsoft.icon')

@app.route('/sitemap')
def sitemap():
    return send_from_directory(os.path.join(app.root_path, 'static'), 'sitemap.xml')

if __name__ == '__main__':
    app.run()
